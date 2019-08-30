import React from 'react';
import { Link } from 'react-router-dom';

import Loading from '../components/Loading';
import Heading from '../components/Heading';

class SecureAreas extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      hasLoaded: false,
      hasErrored: false,
      itemType: "secure",
      itemDisplayName: "Secure Areas"
    }
  }

  async componentDidMount() {
    this.props.fetchData('/api/secureareas', this);
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
                  <th className="am-name">Secure Area Name</th>
                  <th className="am-url">URL</th>
                  <th className="am-appId">AppID</th>
                  <th className="am-ownerId">OwnerID</th>
                </tr>
              </thead>
              <tbody>
                {this.state.response.map((secureArea) => (
                  <tr key={secureArea.id}>
                    <td className="am-cell am-name am-primaryName"><Link className="am-secure" to={"/secure/" + secureArea.id}>{secureArea.description}</Link></td>
                    <td className="am-cell am-url">{secureArea.url}</td>
                    <td className="am-cell am-appId">{secureArea.appId}</td>
                    <td className="am-cell am-ownerId">{secureArea.ownerId}</td>
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

export default SecureAreas;