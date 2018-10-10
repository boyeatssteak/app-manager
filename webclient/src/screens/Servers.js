import React from 'react';
import { connect } from 'react-redux';

import PageHeader from '../components/PageHeader';

class Servers extends React.Component {
  render() {
    return (
      <div className="container">
        <PageHeader title="Servers" />
      </div>
    )
  }
}

export default Servers;