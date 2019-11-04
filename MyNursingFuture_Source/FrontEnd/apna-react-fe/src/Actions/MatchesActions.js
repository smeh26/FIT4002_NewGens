export const declineMatch = (matchId) => {
    return {
        type: 'DELETE_MATCH',
        matchId: matchId
    }
}
export const applyMatch = (matchId) => {
    return {
        type: 'APPLY_MATCH',
        matchId: matchId
    }
}