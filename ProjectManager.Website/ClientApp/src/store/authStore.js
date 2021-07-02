"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.reducer = exports.actionCreators = void 0;
//----------------------
// ACTION CREATORS
exports.actionCreators = {
    requestLogin: function (loginData) { return function (dispatch, getState) {
        var appState = getState();
        if (appState && appState.authState && appState.authState.loggedIn === false) {
            fetch("api/auth/login")
                .then(function (response) { return response.json(); })
                .then(function (data) {
                dispatch({ type: 'RECEIVE_PROFILE_ACTION', data: data });
            });
            dispatch({ type: 'REQUEST_LOGIN_ACTION', data: loginData });
        }
    }; },
    requestRegistration: function (registerData) { return function (dispatch, getState) {
        var appState = getState();
        if (appState && appState.authState && appState.authState.loggedIn === false) {
            fetch("api/auth/register")
                .then(function (response) { return response.json(); })
                .then(function (data) {
                dispatch({ type: 'RECEIVE_PROFILE_ACTION', data: data });
            });
            dispatch({ type: 'REQUEST_REGISTER_ACTION', data: registerData });
        }
    }; }
};
var unloadedState = { loggedIn: false, isLoading: false, profile: undefined };
var reducer = function (state, incomingAction) {
    if (state === undefined) {
        return unloadedState;
    }
    var action = incomingAction;
    switch (action.type) {
        case 'REQUEST_LOGIN_ACTION':
            return {
                loggedIn: false,
                isLoading: true,
                profile: undefined
            };
        case 'REQUEST_REGISTER_ACTION':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            return {
                loggedIn: false,
                profile: undefined,
                isLoading: true
            };
        case 'RECEIVE_PROFILE_ACTION':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            if (state.loggedIn)
                return {
                    loggedIn: true,
                    profile: action.data,
                    isLoading: false
                };
            break;
    }
    return state;
};
exports.reducer = reducer;
//# sourceMappingURL=authStore.js.map