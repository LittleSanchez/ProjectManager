import * as React from 'react';
import { connect } from 'react-redux';

const NotFound = () => (
    <div>
        <h1>Sorry</h1>
        <p>Can't found path</p>
    </div>
);

export default connect()(NotFound);
