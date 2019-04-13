
var currentVideoElementId;
var currentVideoDiv;
$(function () {

    $.fn.carousel.Constructor.prototype.resetSliding = function () {
        this.sliding = false;
    }

    $('#Carousel').carousel({
        interval: false
    });

    var youtube = new YoutubeVideos("UCsUGPFaizVkz8NjX7pBz5Pw", "AIzaSyCvClyummKjtBWOA9Ec1hOjMvqToYYKbxY", "#carousel-inner-videos", "#channel-lists", "#video-container");
    youtube.getPlaylist();

    window.changeVideo = function (id) {
        youtube.changeVideo(id);
    }

    window.showVideosModal = function (el, div) {
        currentVideoElementId = el;
        currentVideoDiv = div;
        $("#videos-modal").modal("show");
    }

    window.selectVideo = function () {
        $("#" + currentVideoDiv).html("<iframe  type=\"text/html\" width=\"640\" height=\"380\" src=\"https://www.youtube.com/embed/" + youtube.currentVideoId + "?autoplay=0\" frameborder=\"0\"></iframe>");
        $("#" + currentVideoElementId).val(youtube.currentVideoId);
        $("#videos-modal").modal("hide");
        $("#video-div").show();
    }
    
});

function SliderAdmin(sliderContainer, videoPerView) {

    var self = this;
    var index = 0;
    var prevRow = null;
    var currentRow = 0;

    function generateSlide(img, title, vidId) {
        var slide = { title: title, url: img, videoId: vidId };
        var videos = 4;
        if (typeof videoPerView != 'undefined') {
            videos = videoPerView;
        }
        if (prevRow == null || index % videos == 0) {
            prevRow = document.createElement("div");
            if (index == 0) {
                $(prevRow).addClass("item active");
                $("#carousel-nav").append("<li data-target=\"#Carousel\" data-slide-to=\"0\" class=\"active\"></li>");
            } else {
                $(prevRow).addClass("item");
                $("#carousel-nav").append("<li data-target=\"#Carousel\" data-slide-to=\"" + currentRow + "\" ></li>");
            }
            currentRow++;
        }
        var slideData = Mustache.to_html($("#slide-div").html(), { Slide: slide });
        $(prevRow).append(slideData);
        index++;
        $(sliderContainer).append(prevRow);

        videosCount = index;
        return true;
    };

    self.addSlide = function (imgSrc, title, listId) {
        return generateSlide(imgSrc, title, listId);
    }

    self.removeSlides = function () {
        $(sliderContainer).html("");
        $("#carousel-nav").html("");
        prevRow = null;
        currentRow = 0;
        index = 0;
    }
}

function YoutubeVideos(channelId, apiKey, sliderContainer, selectInput, videoContainer) {

    var self = this;
    var videoView = 4;

    var sliderAdmin = new SliderAdmin(sliderContainer, videoView);

    var playLists = [];

    var fullLoaded = 0;
    
    function getVideosFromPlaylist(playlistItem, nextToken) {
        if (nextToken == null) {
            nextToken = "";
            playlistItem.videos = [];
        } else {
            nextToken = "&pageToken=" + nextToken;
        }
        $.get("https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&playlistId=" + playlistItem.playList.id + nextToken + "&maxResults=50&key=" + apiKey,
            null, function (data) {
                if (data.nextPageToken != undefined) {
                    getVideosFromPlaylist(playlistItem, data.nextPageToken);
                }
                $.each(data.items, function (i, item) {
                    playlistItem.videos.push(item);
                });
                fullLoaded++;
                if (fullLoaded == playLists.length) {
                    //check if all videos were loaded
                    evalValidLists();
                }
            });
    }

    function evalValidLists() {
        for (var i = 0; i < playLists.length; i++) {
            var playListItem = playLists[i];
            for (var j = 0; j < playListItem.videos.length; j++) {
                var video = playListItem.videos[j];
                if (video.snippet.title.indexOf("Private") > -1) {
                    playListItem.videos.splice(j, 1);
                    j--;
                }
            }
            if (playListItem.videos.length == 0) {
                playLists.splice(i, 1);
                i--;
            }
        }
        generateView();
    }

    function generateView() {
        var selectI = $(selectInput);
        for (var i = 0; i < playLists.length; i++) {
            selectI.append("<option value='" + i + "'>" + playLists[i].playList.snippet.title + "</option>");
        }

        selectI.change(function () {
            changeList($(this).val());
        });
        
        addVideoSliders(0);
        $(videoContainer).append(getYoutubeIframe(playLists[0].videos[0].snippet.resourceId.videoId));
    }

    function addVideoSliders(id) {
        var list = playLists[id].videos;
        for (var i = 0; i < list.length; i++) {
            sliderAdmin.addSlide(list[i].snippet.thumbnails.high.url, list[i].snippet.title, list[i].snippet.resourceId.videoId);
        }
    }

    self.getPlaylist = function () {
        $.get("https://www.googleapis.com/youtube/v3/playlists?part=snippet&channelId=" + channelId + "&maxResults=50&key=" + apiKey,
            null, function (data) {
                $.each(data.items, function (i, item) {

                    var playlistItem = { playList: item, videos: null, show: false, last: false };

                    if (i + 1 == data.items.length) {
                        playlistItem.last = true;
                    }

                    playLists.push(playlistItem);
                    getVideosFromPlaylist(playlistItem);
                });
            });
    }

    self.changeVideo = function (id) {
        $(videoContainer).html("");
        $(videoContainer).append(getYoutubeIframe(id));
    }

    function changeList(id) {
        sliderAdmin.removeSlides();
        addVideoSliders(id);
        var list = playLists[id].videos;
        $(videoContainer).html("");
        $(videoContainer).append(getYoutubeIframe(list[0].snippet.resourceId.videoId));
        $('#Carousel').carousel('resetSliding');
    }

    function getYoutubeIframe(videoId) {
        self.currentVideoId = videoId;
        return "<iframe  type=\"text/html\" width=\"640\" height=\"380\" src=\"https://www.youtube.com/embed/" + videoId + "?autoplay=0\" frameborder=\"0\"/>";
    }
}