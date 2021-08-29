import * as React from 'react';
import Particles from 'react-tsparticles';
import { Container } from 'reactstrap';
import styles from './EmptyLayout.module.scss';


export default class EmptyLayout extends React.PureComponent<{}, { children?: React.ReactNode }> {

    
    constructor(props: any) {
        super(props);

    }

    particlesInit = (main: any) => {
        console.log(main)
    }

    particlesLoaded = (container: any) => {
        console.log(container);
    }


    public render() {
        return (
            <React.Fragment>
                <Container className={`${styles['layout__container']}`} fluid>
                <Particles
        id="tsparticles"
        url="/presets/particles.json"
        init={this.particlesInit}
        loaded={this.particlesLoaded}
      />
                    {this.props.children}
                </Container>
            </React.Fragment>
        );
    }
}