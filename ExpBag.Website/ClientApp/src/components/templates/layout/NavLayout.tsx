import * as React from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from '../navbar';

export default class NavLayout extends React.PureComponent<{}, { children?: React.ReactNode }> {
    public render() {
        return (
            <React.Fragment>
                <Container fluid>
                    <NavMenu /> 
                    {this.props.children}
                </Container>
            </React.Fragment>
        );
    }
}