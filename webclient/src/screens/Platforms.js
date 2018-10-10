import React from 'react';
import { connect } from 'react-redux';

import PageHeader from '../components/PageHeader';

class Platforms extends React.Component {
  render() {
    return (
      <div className="container">
        <PageHeader title="Platforms" />
      </div>
    )
  }
}

export default Platforms;