import React from 'react';
import { Link } from 'react-router-dom';

import Loading from '../components/Loading';
import LargeIcon from '../components/LargeIcon';
import SmallIcon from '../components/SmallIcon'
import NoDetails from '../components/NoDetails';

class ServerDetail extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      hasLoaded: false,
      hasErrored: false,
      itemType: "server",
      itemDisplayName: "Server Detail"
    }
  }

  async componentDidMount() {
    this.props.fetchData(`/api/servers/${this.props.match.params.serverId}`, this )
  }

  render() {
    if (this.state.hasErrored) {
      return <p>Error'd!</p>;
    }

    if (this.state.hasLoaded) {
      return (
        <div className={"container am-" + this.state.itemType}>
          <LargeIcon itemType={this.state.itemType} />
          <section className="am-container am-primary">
            <h3>{this.state.itemDisplayName}</h3>
            <h2 className={"am-" + this.state.itemType}>{this.state.response.server.hostname}<span className="am-domain">.{this.state.response.server.domain}</span></h2>
          </section>
          <section className={"am-container am-details am-" + this.state.itemType}>
            <div><span className="am-detailTitle">IP Address:</span> {this.state.response.server.ipAddress}</div>
            <div><span className="am-detailTitle">OS:</span> {this.state.response.server.opSystem}</div>
            <div><span className="am-detailTitle">Role:</span> {this.state.response.server.role}</div>
            <div><span className="am-detailTitle">Status:</span> {this.state.response.server.status}</div>
          </section>
          <section className="am-container am-related am-instance">
            <SmallIcon itemType="instance" />
            <h3 className="am-section am-instance">Instances on this Server</h3>
            <table className="am-table am-instance">
              <thead>
                <tr>
                  <th className="am-intanceName">Instance</th>
                  <th className="am-app">Application</th>
                  <th className="am-url">URL</th>
                  <th className="am-status">Status</th>
                  <th className="am-user">Owner</th>
                </tr>
              </thead>
              <tbody>
                {this.state.response.instances.length === 0 ?
                  <NoDetails noDetailItem="app instances" noDetailParent={this.state.itemType} /> :
                  this.state.response.instances.map((instance) => (
                    <tr key={instance.id}>
                      <td className="am-cell am-environment am-primaryName">
                        <Link className="am-instance" to={"/instances/" + instance.id}>{instance.env}</Link>
                      </td>
                      <td className="am-cell am-appName">
                        <Link className="am-app" to={"/applications/" + instance.appId}>{instance.appName}</Link>
                      </td>
                      <td className="am-cell am-url">
                        <a href={instance.url} target="_blank">{instance.url}</a>
                      </td>
                      <td className="am-cell am-status">{instance.status}</td>
                      <td className="am-cell am-user">
                        <Link className="am-user" to={"/users/" + instance.ownerId}>{instance.ownerName}</Link>
                      </td>
                    </tr>
                ))}
              </tbody>
            </table>
          </section>
        </div>
      )
    }

    return <Loading loadingItem={this.state.itemDisplayName} />;

  }
}

export default ServerDetail;