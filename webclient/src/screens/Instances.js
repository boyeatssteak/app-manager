import React from 'react';
import { Link } from 'react-router-dom';

import Loading from '../components/Loading';
import Heading from '../components/Heading';

class Instances extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      hasLoaded: false,
      hasErrored: false,
      itemType: "instance",
      itemDisplayName: "Instances"
    }
  }

  async componentDidMount() {
    this.props.fetchData('/api/instances', this);
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
                  <th className="am-url">URL</th>
                  <th className="am-status">Status</th>
                </tr>
              </thead>
              <tbody>
                {this.state.response.map((instance) => (
                  <tr key={instance.id}>
                    <td className="am-cell am-name am-primaryName"><Link className="am-instance" to={"/instances/" + instance.id}>{instance.name}</Link></td>
                    <td className="am-cell am-url"><a href={instance.url} target="_blank">{instance.url}</a></td>
                    <td className="am-cell am-status">{instance.status}</td>
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

export default Instances;