import React from 'react';
import { connect } from 'react-redux';

import { itemsFetchData } from '../actions/items';
import PageHeader from '../components/PageHeader';

class Servers extends React.Component {

  componentDidMount() {
    this.props.fetchData('/api/servers');
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
        <PageHeader title="Servers" />
        <ul>
          {this.props.servers.map((server) => (
            <li key={server.id}>
              {server.hostname} - {server.ipAddress} ({server.opSystem})
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
    servers: state.items,
    hasErrored: state.itemsHasErrored,
    isLoading: state.itemsIsLoading
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    fetchData: (url) => dispatch(itemsFetchData(url))
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Servers);