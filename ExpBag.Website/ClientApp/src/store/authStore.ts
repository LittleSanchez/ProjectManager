import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';


export interface AuthState {
    loggedIn: boolean,
    isLoading: boolean,
    profile?: AuthProfile,
}


//It's a profile model of your project, change if needed
export interface AuthProfile {
    username: string,
    email: string,
    token: string,
}
//----------------------
// API MODELS

interface LoginModel {
    email: string,
    password: string
}

interface RegisterModel {
    displayName : string,
    email: string,
    password: string
}

//---------------------
// ACTIONS

interface RequestLoginAction {
    type: 'REQUEST_LOGIN_ACTION',
    data: LoginModel
}

interface RequestRegisterAction {
    type: "REQUEST_REGISTER_ACTION",
    data: RegisterModel
}

interface ReceiveProfileAction {
    type: 'RECEIVE_PROFILE_ACTION',
    data: AuthProfile
}

interface LogOutAction {
    type: 'LOG_OUT_ACTION',
    data: undefined
}

//---------------
// KNOWN TYPE

type KnownAction =
    RequestLoginAction |
    RequestRegisterAction |
    ReceiveProfileAction |
    LogOutAction;

//----------------------
// ACTION CREATORS

export const actionCreators = {
    requestLogin: (loginData: LoginModel): AppThunkAction<KnownAction> => (dispatch, getState) => {
        console.log("Auth state request login")
        console.log(loginData);
        const authState = getState().auth;
        if (authState && authState.loggedIn === false) {
            console.log(authState);
            fetch(`api/auth/login`, {
                method: 'POST',
                body: JSON.stringify(loginData as any),
                headers: {
                    'Content-Type': 'application/json'
                },
            })
                .then(response => response.json() as Promise<AuthProfile>)
                .then(data => {
                    console.log(data);
                    dispatch({ type: 'RECEIVE_PROFILE_ACTION', data: data });
                });

            dispatch({ type: 'REQUEST_LOGIN_ACTION', data: loginData });
        }
    },
    requestRegistration: (registerData: RegisterModel): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.auth && appState.auth.loggedIn === false) {
            fetch(`api/auth/register`, {
                method: 'POST',
                body: JSON.stringify(registerData as any),
                headers: {
                    'Content-Type': 'application/json'
                },
            })
                .then(response => response.json() as Promise<AuthProfile>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_PROFILE_ACTION', data: data });
                });

            dispatch({ type: 'REQUEST_REGISTER_ACTION', data: registerData });
        }
    }
}


const unloadedState: AuthState = { loggedIn: false, isLoading: false, profile: undefined };

export const reducer: Reducer<AuthState> = (state: AuthState | undefined, incomingAction: Action): AuthState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
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
            return {
                ...state,
                loggedIn: false,
                profile: undefined
            }
        default:
            return state;
    }
};