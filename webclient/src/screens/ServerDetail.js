import React from 'react';
import { Link } from 'react-router-dom';

import Loading from '../components/Loading';
import Heading from '../components/Heading';
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
          <Heading headingType="detail" itemType={this.state.itemType} itemDisplayName={this.state.itemDisplayName} title={this.state.response.server.hostname} domain={this.state.response.server.domain} />
          <section className={"am-container am-details am-" + this.state.itemType}>
            <div className="am-row">
              <div><span className="am-detailTitle">IP Address:</span> {this.state.response.server.ipAddress}</div>
              <div><span className="am-detailTitle">OS:</span> {this.state.response.server.opSystem}</div>
              <div><span className="am-detailTitle">Role:</span> {this.state.response.server.role}</div>
              <div><span className="am-detailTitle">Status:</span> {this.state.response.server.status}</div>
            </div>
          </section>
          <section className="am-container am-related am-instance">
            <Heading headingType="related" itemType="instance" title="Instances on this Server" />
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
                        <Link className="am-app" to={"/apps/" + instance.appId}>{instance.appName}</Link>
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