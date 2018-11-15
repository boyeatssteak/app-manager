import React from 'react';
import { Link } from 'react-router-dom';

import Loading from '../components/Loading';
import NoDetails from '../components/NoDetails';
import Heading from '../components/Heading';

class AppDetail extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      hasLoaded: false,
      hasErrored: false,
      itemType: "app",
      itemDisplayName: "Application Detail"
    }
  }

  async componentDidMount() {
    this.props.fetchData(`/api/applications/${this.props.match.params.appId}`, this )
  }

  render() {
    if (this.state.hasErrored) {
      return <p>Error'd!</p>;
    }

    if (this.state.hasLoaded) {
      return (
        <div className={"container am-" + this.state.itemType}>
          <Heading headingType="detail" itemType={this.state.itemType} itemDisplayName={this.state.itemDisplayName} title={this.state.response.application[0].name} />
          <section className="am-container">
            <p>{this.state.response.application[0].desc}</p>
            <p><span className="am-detailTitle">Code Repo:</span><a href={this.state.response.application[0].repo} target="_blank">{this.state.response.application[0].repo}</a></p>
          </section>
          <section className={"am-container am-details am-" + this.state.itemType}>
            <div className="am-row">
              <div><span className="am-detailTitle">Platform:</span>
                <Link className="am-platform" to={"/platforms/" + this.state.response.application[0].platformId}>{this.state.response.application[0].platform}</Link>
              </div>
              <div><span className="am-detailTitle">Audience:</span>{this.state.response.application[0].audience}</div>
              <div><span className="am-detailTitle">Owner:</span>
                <Link className="am-user" to={"/users/" + this.state.response.application[0].ownerId}>{this.state.response.application[0].owner}</Link>
              </div>
              <div><span className="am-detailTitle">Status:</span>{this.state.response.application[0].status}</div>
            </div>
          </section>
          <section className="am-container am-related am-instance">
            <Heading headingType="related" itemType="instance" title="Instances of this Application" />
            <table className="am-table am-instance">
              <thead>
                <tr>
                  <th className="am-intanceName">Environment</th>
                  <th className="am-serverNames">Servers</th>
                  <th className="am-url">URL</th>
                  <th className="am-status">Status</th>
                </tr>
              </thead>
              <tbody>
                {this.state.response.instances.length === 0 ?
                  <NoDetails noDetailItem="instances" noDetailParent={this.state.itemType} /> :
                  this.state.response.instances.map((instance) => (
                    <tr key={instance.id}>
                      <td className="am-cell am-environment am-primaryName">
                        <Link className="am-instance" to={"/instances/" + instance.id}>{instance.env}</Link>
                      </td>
                      <td className="am-cell am-serverNames">
                        {instance.servers.map((server) => (
                          <Link to={"/servers/" + server.id} key={server.id} className="am-server" title={server.hostname + "." + server.domain + " (" + server.ipAddress + ")"}>{server.hostname}</Link> 
                        ))}
                      </td>
                      <td className="am-cell am-url">
                        <a href={instance.url} target="_blank">{instance.url}</a>
                      </td>
                      <td className="am-cell am-status">{instance.status}</td>
                    </tr>
                ))}
              </tbody>
            </table>
          </section>
          <section className="am-container am-related am-secureArea">
            <Heading headingType="related" itemType="secure" title="Secure Areas of this Application" />
            <table className="am-table am-secure">
              <thead>
                <tr>
                  <th className="am-secureName">Name</th>
                  <th className="am-url">URL</th>
                  <th className="am-owner">Owner</th>
                </tr>
              </thead>
              <tbody>
                {this.state.response.secureAreas.length === 0 ?
                  <NoDetails noDetailItem="secure areas" noDetailParent={this.state.itemType} /> :
                  this.state.response.secureAreas.map((secureArea) => (
                    <tr key={secureArea.id}>
                      <td className="am-cell am-secureName am-primaryName">
                        <Link className="am-secure" to={"/secureareas/" + secureArea.id}>{secureArea.name}</Link>
                      </td>
                      <td className="am-cell am-url">
                        <a href={secureArea.url} target="_blank">{secureArea.url}</a>
                      </td>
                      <td className="am-cell am-owner">
                        <Link className="am-user" to={"/users/" + secureArea.ownerId}>{secureArea.owner}</Link>
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

export default AppDetail;