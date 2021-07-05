import * as React from "react";
import { connect } from "react-redux";
import { ApplicationState } from "../../store";
import * as AuthStore from "../../store/authStore";

type RegistrationProps =
    AuthStore.AuthState // ... state we've requested from the Redux store
    & typeof AuthStore.actionCreators // ... plus ation creators we've requested
//& RouteComponentProps<{ startDateIndex: string }>; // .

class Registration extends React.PureComponent<RegistrationProps> {

    public constructor(props) {
        super(props);
    }

    public componentDidMount = () => {
        console.log("Registration mounted");
    }

    public componentDidUpdate = () => {
        console.log("Registration updated");
    }

    protected onFormSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        this.props.requestRegistration({
            displayName: e.currentTarget['display-name'].value,
            email: e.currentTarget['email'].value,
            password: e.currentTarget['password'].value
        });
    }

    public render = () => {
        return (
            <React.Fragment>
                <div className="row justify-content-center">
                     <div className="col-4">
                        <form method="POST" action="#" onSubmit={this.onFormSubmit}>
                            <div className="form-group">
                                <input className="form-control" type="text" name="display-name" id="display-name" placeholder="Display name" />
                            </div>
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
    (state: ApplicationState) => state.auth,
    AuthStore.actionCreators
)(Registration as any);
