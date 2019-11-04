const initState = {
    matches : [
        {matchId: '1', employer: 'Hospital 1', title: 'job listing 1', location: 'Australia', closeDate: '1/1/2020', isApplied: false},
        {matchId: '2', employer: 'Hospital 2', title: 'job listing 2', location: 'Australia', closeDate: '1/1/2020', isApplied: false},
        {matchId: '3', employer: 'Hospital 3', title: 'job listing 3', location: 'Australia', closeDate: '1/1/2020', isApplied: false} 
    ]
}

const MatchesReducer = (state = initState, action) => {
    switch(action.type) {
        case 'FETCH_USER_MATCHES':

        case 'DELETE_MATCH':
            let newMatches = state.matches.filter(match => {
                return match.matchId !== action.matchId
            });
            return {
                ...state,
                matches: newMatches
            };
        case 'ADD_MATCH':
            return {
                ...state,
                matches: [...this.state.matches, action.match]
            };
        case 'APPLY_MATCH':
            let applyMatch = state.matches.map(match => {
                if (match.matchId === action.matchId){
                    match.isApplied = true
                    return match
                }
                else {
                    return match
                }
            })
            return {
                ...state,
                matches: applyMatch
            }
        default:
            return state;
    }
}

export default MatchesReducer;