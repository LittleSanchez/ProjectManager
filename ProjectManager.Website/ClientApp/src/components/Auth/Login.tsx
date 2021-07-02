import React, { FormEvent, useEffect } from "react";
import { connect } from "react-redux";
import { ApplicationState } from "../../store";
import * as AuthStore from "../../store/authStore";

type LoginProps =
    AuthStore.AuthState // ... state we've requested from the Redux store
    & typeof AuthStore.actionCreators // ... plus action creators we've requested
    //& RouteComponentProps<{ startDateIndex: string }>; // .

class Login extends React.PureComponent<LoginProps> {

    public Login() {
        useEffect(() => {
            console.log("Hello Login");
        })
    }

    protected onFormSubmit(e: FormEvent) {
        e.preventDefault();
        this.props.requestLogin(
            {
                login: e.target['login'].value,
                password: e.target['password'].value
            });
    }

    public render() {
        return (
            <form method="POST" action="#" onSubmit={this.onFormSubmit}>
                <input type="text" name="login" id="login" />
                <input type="password" name="password" id="password" />
            </form>
        );
    }

}

export default connect(
    (state: ApplicationState) => state.authState,
    AuthStore.actionCreators)(Login as any);
