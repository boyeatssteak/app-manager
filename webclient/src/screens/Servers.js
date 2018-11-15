import React from 'react';
import { Link } from 'react-router-dom';

import Loading from '../components/Loading';
import Heading from '../components/Heading';

class Servers extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      hasLoaded: false,
      hasErrored: false,
      itemType: "server",
      itemDisplayName: "Servers"
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
        <div className={"container am-" + this.state.itemType}>
          <Heading headingType="main" itemType={this.state.itemType} itemDisplayName={this.state.itemDisplayName} />
          <section className="am-container">
            <table className={"am-table am-" + this.state.itemType}>
              <thead>
                <tr>
                  <th className="am-hostname">Hostname</th>
                  <th className="am-domain">Domain</th>
                  <th className="am-ipAddress">IP Address</th>
                  <th className="am-opSystem">OS</th>
                  <th className="am-role">Role</th>
                  <th className="am-status">Status</th>
                </tr>
              </thead>
              <tbody>
                {this.state.response.map((server) => (
                  <tr key={server.id}>
                    <td className="am-cell am-hostname am-primaryName"><Link className="am-server" to={"/servers/" + server.id}>{server.hostname}</Link></td>
                    <td className="am-cell am-domain">{server.domain}</td>
                    <td className="am-cell am-ipAddress">{server.ipAddress}</td>
                    <td className="am-cell am-opSystem">{server.opSystem}</td>
                    <td className="am-cell am-role">{server.role}</td>
                    <td className="am-cell am-status">{server.status}</td>
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

export default Servers;