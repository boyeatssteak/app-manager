import React from 'react';
import { Link } from 'react-router-dom';

import Loading from '../components/Loading';
import Heading from '../components/Heading';
import NoDetails from '../components/NoDetails';

class InstanceDetail extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      hasLoaded: false,
      hasErrored: false,
      itemType: "instance",
      itemDisplayName: "Instance Detail"
    }
  }

  async componentDidMount() {
    this.props.fetchData(`/api/instances/${this.props.match.params.instanceId}`, this )
  }

  render() {
    if (this.state.hasErrored) {
      return <p>Error'd!</p>;
    }

    if (this.state.hasLoaded) {
      return (
        <div className={"container am-" + this.state.itemType}>
          <Heading headingType="detail" itemType={this.state.itemType} itemDisplayName={this.state.itemDisplayName} title={this.state.response.instance.name} />
          <section className={"am-container am-details am-" + this.state.itemType}>
            <div className="am-row">
              <div><span className="am-detailTitle">Env:</span> {this.state.response.instance.environment}</div>
              <div><span className="am-detailTitle">URL:</span> <a href={this.state.response.instance.url} target="_blank">{this.state.response.instance.url}</a></div>
              <div><span className="am-detailTitle">Status:</span> {this.state.response.instance.status}</div>
            </div>
          </section>
          <section className="am-container am-related am-app">
            <Heading headingType="related" itemType="app" title={this.state.response.application[0].name} />
            <div className="am-row am-details">
              <div>
                <Link className="am-app" to={"/apps/" + this.state.response.application[0].id}>App Details</Link>
              </div>
              <div><span className="am-detailTitle">Platform:</span>
                <Link className="am-platform" to={"/platforms/" + this.state.response.application[0].platformId}>{this.state.response.application[0].platform}</Link>
              </div>
              <div><span className="am-detailTitle">Owner:</span>
                <Link className="am-user" to={"/users/" + this.state.response.application[0].ownerId}>{this.state.response.application[0].owner}</Link>
              </div>
            </div>
          </section>
          <section className="am-container am-related am-server">
            <Heading headingType="related" itemType="server" title="Servers utilized" />
            <table className="am-table am-server">
              <thead>
                <tr>
                  <th className="am-hostname">Hostname</th>
                  <th className="am-domain">Domain</th>
                  <th className="am-ipAddress">IP</th>
                  <th className="am-opSystem">OS</th>
                  <th className="am-role">Role</th>
                </tr>
              </thead>
              <tbody>
                {this.state.response.servers.length === 0 ?
                  <NoDetails noDetailItem="servers" noDetailParent={this.state.itemType} /> :
                  this.state.response.servers.map((server) => (
                    <tr key={server.id}>
                      <td className="am-cell am-hostname am-primaryName">
                        <Link className="am-server" to={"/servers/" + server.id}>{server.hostname}</Link>
                      </td>
                      <td className="am-cell am-domain">{server.domain}</td>
                      <td className="am-cell am-ipAddress">{server.ipAddress}</td>
                      <td className="am-cell am-opSystem">{server.opSystem}</td>
                      <td className="am-cell am-role">{server.role}</td>
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

export default InstanceDetail;