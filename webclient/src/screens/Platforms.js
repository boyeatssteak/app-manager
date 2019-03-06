import React from 'react';
import { Link } from 'react-router-dom';

import Loading from '../components/Loading';
import Heading from '../components/Heading';

class Platforms extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      hasLoaded: false,
      hasErrored: false,
      itemType: "platform",
      itemDisplayName: "Platforms"
    }
  }

  async componentDidMount() {
    this.props.fetchData('/api/platforms', this);
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
                  <th className="am-name">Name</th>
                  <th className="am-vendor">Vendor</th>
                  <th className="am-description">Description</th>
                </tr>
              </thead>
              <tbody>
                {this.state.response.map((platform) => (
                  <tr key={platform.id}>
                    <td className="am-cell am-name am-primaryName"><Link className="am-platform" to={"/platforms/" + platform.id}>{platform.name}</Link></td>
                    <td className="am-cell am-vendor">{platform.vendorId}</td>
                    <td className="am-cell am-description">{platform.description}</td>
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

export default Platforms;