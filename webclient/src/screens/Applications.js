import React from 'react';
import { connect } from 'react-redux';

import PageHeader from '../components/PageHeader';

class Applications extends React.Component {
  render() {
    return (
      <div className="container">
        <PageHeader title="Applications" />
      </div>
    )
  }
}

export default Applications;