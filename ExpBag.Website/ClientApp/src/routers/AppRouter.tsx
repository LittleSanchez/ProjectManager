import * as React from 'react'
import { Route, Switch } from 'react-router';
import { EmptyLayout, NavLayout } from '../components/templates/layout';

import { Home, NotFound, Login, Registration } from '../pages';


export default function AppRouter() {
    return (<>
        <Switch>
            <Switch>
                <Route path="/auth">
                    <EmptyLayout>
                        <Route exact path="/auth/login" component={Login} />
                        <Route
                            exact
                            path="/auth/registration"
                            component={Registration}
                        />
                    </EmptyLayout>
                </Route>
                <Route path="/">
                    <NavLayout>
                        <Switch>
                            <Route exact path="/" component={Home} />
                            {/*<Route exact path='/about' component={About} />*/}
                            <Route component={NotFound} />
                        </Switch>
                    </NavLayout>
                </Route>
            </Switch>
        </Switch>
    </>
    );
}
