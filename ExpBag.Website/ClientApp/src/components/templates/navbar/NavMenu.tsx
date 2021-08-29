import * as React from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import styles from './NavMenu.module.scss';
import ReactAnime from 'react-animejs';

const { Anime } = ReactAnime;



export default class NavMenu extends React.PureComponent<{}, { isOpen: boolean }> {
    public state = {
        isOpen: false
    };

    public render() {
        return (
            <Anime>

                <header>

                    <div className={`${styles['navbar']} d-flex justify-content-between`}>

                        <Link className={`${styles['navbar__logo']} d-flex align-items-center`} to="/">
                            <img className={`${styles['navbar__logo-img']}`} src="/img/logo-light.svg" alt="logo light" />
                            <span className={`${styles['navbar__logo-text']} logo-type`}>expbag</span>
                        </Link>
                        <ul className={`${styles['navbar__menu']} list-unstyled d-flex align-items-center`}>
                            <li>
                                <Link to="/auth/login">Log in</Link>
                            </li>
                        </ul>
                    </div>
                </header>
            </Anime>
        );
    }

    private toggle = () => {
        this.setState({
            isOpen: !this.state.isOpen
        });
    }
}
