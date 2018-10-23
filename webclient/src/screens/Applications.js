import React from 'react';
import { connect } from 'react-redux';

import { itemsFetchData } from '../actions/items';
import PageHeader from '../components/PageHeader';

class Applications extends React.Component {

  componentDidMount() {
    this.props.fetchData('/api/applications');
  }

  render() {
    if (this.props.hasErrored) {
      return <p>Error'd!</p>;
    }

    if (this.props.isLoading) {
      return <p>Loading...</p>;
    }

    return (
      <div className="container">
        <PageHeader title="Applications" />
        <ul>
          {this.props.apps.map((app) => (
            <li key={app.id}>
              {app.name} - {app.status}
            </li>
          ))}
        </ul>
      </div>
    )
  }
}

const mapStateToProps = (state) => {
  return {
    ...state,
    apps: state.items,
    hasErrored: state.itemsHasErrored,
    isLoading: state.itemsIsLoading
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    fetchData: (url) => dispatch(itemsFetchData(url))
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Applications);