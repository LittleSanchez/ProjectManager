import * as React from 'react';
import { AppRouter } from './routers'

export default function App() {    
    return (
        <React.Fragment>
            <AppRouter />
            {/*<Route path='/counter' component={Counter} />*/}
            {/*<Route path='/fetch-data/:startDateIndex?' component={FetchData} />*/}
        </React.Fragment>
    );
}