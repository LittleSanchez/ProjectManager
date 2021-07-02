import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import About from './components/About';

import './custom.css'
import Login from './components/Auth/Login';

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/about' component={About} />
        <Route exact path='/auth/login' component={Login}/>
        {/*<Route path='/counter' component={Counter} />*/}
        {/*<Route path='/fetch-data/:startDateIndex?' component={FetchData} />*/}
    </Layout>
);
