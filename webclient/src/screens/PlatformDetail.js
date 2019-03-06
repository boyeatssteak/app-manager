import React from 'react';
import { Link } from 'react-router-dom';

import Loading from '../components/Loading';
import Heading from '../components/Heading';
import NoDetails from '../components/NoDetails';

class PlatformDetail extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      hasLoaded: false,
      hasErrored: false,
      itemType: "platform",
      itemDisplayName: "Platform Detail"
    }
  }

  async componentDidMount() {
    this.props.fetchData(`/api/platforms/${this.props.match.params.platformId}`, this )
  }

  render() {
    if (this.state.hasErrored) {
      return <p>Error'd!</p>;
    }

    if (this.state.hasLoaded) {
      const { platform: plat, applications: apps } = this.state.response;
      return (
        <div className={"container am-" + this.state.itemType}>
          <Heading headingType="detail" itemType={this.state.itemType} itemDisplayName={this.state.itemDisplayName} title={plat[0].name} />
          <section className="am-container">
            <p>{plat[0].desc}</p>
            <p><span className="am-detailTitle">Vendor Docs:</span><a href={plat[0].vendorDocs} title={"Documentation provided by the vendor " + plat[0].vendor} target="_blank">{plat[0].vendorDocs}</a></p>
            <p><span className="am-detailTitle">Internal Docs:</span><a href={plat[0].internalDocs} title="Documentation created internally by our company" target="_blank">{plat[0].internalDocs}</a></p>
            <p className="am-details"><span className="am-detailTitle">Vendor:</span>
              <Link className="am-vendor" to={"/vendors/" + plat[0].vendorId}>{plat[0].vendor}</Link>
            </p>
          </section>
          <section className="am-container am-related am-app">
            <Heading headingType="related" itemType="app" title="Applications based on this Platform" displayAddNew />
            <table className="am-table am-app">
              <thead>
                <tr>
                  <th className="am-appName">Name</th>
                  <th className="am-status">Status</th>
                  <th className="am-owner">Owner</th>
                </tr>
              </thead>
              <tbody>
                {apps.length === 0 ?
                  <NoDetails noDetailItem="app" noDetailParent={this.state.itemType} /> :
                  apps.map((app) => (
                    <tr key={app.id}>
                      <td className="am-cell am-appName am-primaryName">
                        <Link className="am-app" to={"/apps/" + app.id} title={app.desc}>{app.name}</Link>
                      </td>
                      <td className="am-cell am-status">{app.status}</td>
                      <td className="am-cell am-owner">
                        <Link className="am-user" to={"/users/" + app.ownerId}>{app.owner}</Link>
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

export default PlatformDetail;