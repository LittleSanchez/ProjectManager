import * as React from 'react';
import { Route, Switch } from 'react-router';
import { NavLayout, EmptyLayout } from './components/templates/layout';
import NotFound from './components/pages/NotFound';
import Login from './components/pages/auth/Login';

import './custom.css'
import Registration from './components/pages/auth/Registration';
import Home from './components/pages/Home';

export default () => (
    <React.Fragment>
        <Switch>
            <Switch>
                <Route path='/auth'>
                    <EmptyLayout>
                        <Route exact path='/auth/login' component={Login} />
                        <Route exact path='/auth/registration' component={Registration} />
                    </EmptyLayout>
                </Route>
                <Route path='/'>
                    <NavLayout>
                        <Switch>
                            <Route exact path='/' component={Home} />
                            {/*<Route exact path='/about' component={About} />*/}
                        </Switch>
                    </NavLayout>
                </Route>
            </Switch>
            <Route component={NotFound} />
        </Switch>
        {/*<Route path='/counter' component={Counter} />*/}
        {/*<Route path='/fetch-data/:startDateIndex?' component={FetchData} />*/}
    </React.Fragment>
);
