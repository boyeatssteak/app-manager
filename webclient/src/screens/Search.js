import React from 'react';
import { connect } from 'react-redux';

import PageHeader from '../components/PageHeader';

class Search extends React.Component {
  render() {
    return (
      <div className="container">
        <PageHeader title="Search" />
      </div>
    )
  }
}

export default Search;