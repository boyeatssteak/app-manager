import React from 'react';
import { Link } from 'react-router-dom';

import PageHeader from '../components/PageHeader';

class Servers extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div className="container">
        <PageHeader title="Servers" />
        <ServersContent fetchData={this.props.fetchData} />
      </div>
    )
  }
}

class ServersContent extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      hasLoaded: false,
      hasErrored: false
    }
  }

  async componentDidMount() {
    this.props.fetchData('/api/servers', this )
  }

  render() {
    if (this.state.hasErrored) {
      return <p>Error'd!</p>;
    }

    if (this.state.hasLoaded) {
      return (
        <div>
          <ul>
            {this.state.response.map((server) => (
              <li key={server.id}>
                <Link to={"/servers/" + server.id}>{server.hostname}</Link> - {server.ipAddress} ({server.opSystem})
              </li>
            ))}
          </ul>
        </div>
      )
    }

    return <p>Loading...</p>;

  }
}

export default Servers;