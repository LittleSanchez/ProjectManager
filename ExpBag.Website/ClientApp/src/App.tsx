import * as React from 'react';
import { Route, Switch } from 'react-router';
import { NavLayout, EmptyLayout } from './components/templates';
import NotFound from './components/NotFound';
import Home from './components/Home';
import About from './components/About';
import Login from './components/Auth/Login';

import './custom.css'
import Registration from './components/Auth/Registration';

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
                            <Route exact path='/about' component={About} />
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
