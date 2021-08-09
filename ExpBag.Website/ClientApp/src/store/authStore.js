"use strict";
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.reducer = exports.actionCreators = void 0;
//----------------------
// ACTION CREATORS
exports.actionCreators = {
    requestLogin: function (loginData) { return function (dispatch, getState) {
        console.log("Auth state request login");
        console.log(loginData);
        var authState = getState().auth;
        if (authState && authState.loggedIn == false) {
            console.log(authState);
            fetch("api/auth/login", {
                method: 'POST',
                body: JSON.stringify(loginData),
                headers: {
                    'Content-Type': 'application/json'
                },
            })
                .then(function (response) { return response.json(); })
                .then(function (data) {
                console.log(data);
                dispatch({ type: 'RECEIVE_PROFILE_ACTION', data: data });
            });
            dispatch({ type: 'REQUEST_LOGIN_ACTION', data: loginData });
        }
    }; },
    requestRegistration: function (registerData) { return function (dispatch, getState) {
        var appState = getState();
        if (appState && appState.auth && appState.auth.loggedIn === false) {
            fetch("api/auth/register", {
                method: 'POST',
                body: JSON.stringify(registerData),
                headers: {
                    'Content-Type': 'application/json'
                },
            })
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
            return {
                loggedIn: true,
                profile: action.data,
                isLoading: false
            };
        case 'LOG_OUT_ACTION':
            return __assign(__assign({}, state), { loggedIn: false, profile: undefined });
        default:
            return state;
    }
};
exports.reducer = reducer;
//# sourceMappingURL=authStore.js.map