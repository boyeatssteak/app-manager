import React from 'react';

import PageHeader from '../components/PageHeader';

class ServerDetail extends React.Component {
  render() {
    return (
      <div className="container">
        <PageHeader title="Server Details" />
        <ServerDetailContent serverId={this.props.match.params.serverId} fetchData={this.props.fetchData} />
      </div>
    )
  }
}

class ServerDetailContent extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      hasLoaded: false,
      hasErrored: false
    }
  }

  async componentDidMount() {
    this.props.fetchData(`/api/servers/${this.props.serverId}`, this )
  }

  render() {
    if (this.state.hasErrored) {
      return <p>Error'd!</p>;
    }

    if (this.state.hasLoaded) {
      return (
        <div>
          <h3>Servername: </h3>
          <p>{this.state.response.server.ipAddress}</p>
          <p>{this.state.response.server.opSystem}</p>
          <p>{this.state.response.server.role}</p>
          <p>{this.state.response.server.status}</p>
          <p>{this.state.response.server.domain}</p>
          <h5>{this.props.serverId}</h5>
        </div>
      )
    }

    return <p>Loading...</p>;

  }
}

export default ServerDetail;