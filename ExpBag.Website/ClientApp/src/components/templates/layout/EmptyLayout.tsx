import * as React from 'react';
import { Container } from 'reactstrap';

export default class EmptyLayout extends React.PureComponent<{}, { children?: React.ReactNode }> {
    public render() {
        return (
            <React.Fragment>
                <Container fluid>
                    {this.props.children}
                </Container>
            </React.Fragment>
        );
    }
}