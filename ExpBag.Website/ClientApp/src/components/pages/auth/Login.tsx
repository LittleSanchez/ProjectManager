import * as React from "react";
import { connect } from "react-redux";
import { ConnectedRouterProps, connectRouter } from 'connected-react-router'
import { ApplicationState } from "../../../store";
import * as AuthStore from "../../../store/authStore";

type LoginProps =
    AuthStore.AuthState // ... state we've requested from the Redux store
    & typeof AuthStore.actionCreators
    & ConnectedRouterProps// ... plus ation creators we've requested
//& RouteComponentProps<{ startDateIndex: string }>; // .



class Login extends React.PureComponent<LoginProps> {

    public constructor(props: LoginProps) {
        super(props);
    }

    public componentDidMount = () => {
        console.log("Login mounted");
    }

    public componentDidUpdate = () => {
        console.log("Login updated");
    }

    protected onFormSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        console.log('1')
        this.props.requestLogin({
            email: (e.currentTarget as any)['email'].value,
            password: (e.currentTarget as any)['password'].value
        });
        this.props.history.push('/');
        console.log('2');
    }

    public render = () => {
        return (
            <React.Fragment>
                <div className="row justify-content-center">
                    <div className="col-4">
                        <form method="POST" action="#" onSubmit={this.onFormSubmit}>
                            <div className="form-group">
                                <input className="form-control" type="text" name="email" id="email" placeholder="Email" />
                            </div>
                            <div className="form-group">
                                <input className="form-control" type="password" name="password" id="password" placeholder="Password" />
                            </div>
                            <div className="form-group">
                                <input className="form-control" type="submit" value="Log in" />
                            </div>
                        </form>
                    </div>
                </div>
            </React.Fragment>
        );
    }

}

export default connect(
    (state: ApplicationState) => ({
        auth: state.auth,
        router: state.router
    }),
    AuthStore.actionCreators,
)(Login as any);
