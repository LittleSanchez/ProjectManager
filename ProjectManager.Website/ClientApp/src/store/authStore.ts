import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';


export interface AuthState {
    loggedIn: boolean,
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
    username: string,
    password: string
}

interface RegisterModel {
    username: string,
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

//---------------
// KNOWN TYPE

type KnownAction =
    RequestLoginAction |
    RequestRegisterAction |
    ReceiveProfileAction;

//----------------------
// ACTION CREATORS

export const actionCreators = {
    requestLogin: (data: LoginModel): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.authState) {
            //TODO: Make normal auth reducer
        }
    }
}