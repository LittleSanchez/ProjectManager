import * as React from 'react';
import { NavMenu } from '../navbar';
import EmptyLayout from './EmptyLayout';

export default class NavLayout extends React.PureComponent<{}, { children?: React.ReactNode }> {
    public render() {
        return (
            <React.Fragment>
                <EmptyLayout>
                    <NavMenu/>
                    {this.props.children}
                </EmptyLayout>
            </React.Fragment>
        );
    }
}