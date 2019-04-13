import React, { Component } from 'react';
import { connect } from 'react-redux';
import { fetchFrameworkData } from '../../Actions';
import config from '../../config';

class Endorsedlogo extends Component {
    constructor(props) {
        super(props);
    }

    componentWillMount() {
        this.props.onMount();
    }

    render() {
        if (this.props.loading) { return <div></div> }
        else {
            let endorsedLogos = this.props.endorsedLogos;
            if (!endorsedLogos) { return <div></div> }
            return (
                <div>
                    <h2>Endorsed by:</h2>

                    <div className="row endorsed endorsedLogoLg">
                        {endorsedLogos.map((lo, index) =>
                            <div className="col-6 col-sm-3" key={index}>
                                <img src={config.siteUrl + config.imagesDirectory + lo.image} />
                            </div>
                        )}

                    </div>
                </div>
            );
        }
    };

}

const mapStateToProps = (state) => {
    return {
        loading: state.app.framework.domainDataLoading,
        endorsedLogos: state.app.framework.endorsedLogos
    }
};

const mapDispatchToProps = (dispatch, props) => {
    return {
        onMount: function () {
            dispatch(fetchFrameworkData());
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Endorsedlogo);




