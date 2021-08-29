import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as AuthStore from '../store/authStore';
import styles from './Home.module.scss';

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
                <section className={`${styles['header']} row justify-content-center`}>
                    <div className="col-md-8 col-lg-6 ">
                        <h1 className={`${styles['header__title']} logo-type`}>expbag</h1>
                        <p className={`${styles['header__subtitle']}`}>The best way to manage your experience</p>
                    </div>
                </section>
            </React.Fragment>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.auth,
    AuthStore.actionCreators)(Home as any);
