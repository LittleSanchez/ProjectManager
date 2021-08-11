import { ConnectedRouterProps } from 'connected-react-router';
import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import * as AuthStore from '../../store/authStore';

type HomeProps = AuthStore.AuthState
    & typeof AuthStore.actionCreators;

// type HomeState = {};


class Home extends React.PureComponent<HomeProps> {

    public constructor(props: HomeProps) {
        super(props);
    }
    
    public componentDidMount = () => {
        console.log("Home did mount")
    }

    public componentDidUpdate = () => {

    }

    public render = () => {
        return ( <React.Fragment>
                <h1>ADEPTE XIAAOOOOOOO</h1>
            </React.Fragment>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.auth,
    AuthStore.actionCreators)(Home as any);
