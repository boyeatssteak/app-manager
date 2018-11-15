import React from 'react';
import { Link } from 'react-router-dom';

import Loading from '../components/Loading';
import Heading from '../components/Heading';

class Applications extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      hasLoaded: false,
      hasErrored: false,
      itemType: "app",
      itemDisplayName: "Applications"
    }
  }

  async componentDidMount() {
    this.props.fetchData('/api/applications', this);
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
                  <th className="am-repo">Repo</th>
                  <th className="am-status">Status</th>
                </tr>
              </thead>
              <tbody>
                {this.state.response.map((app) => (
                  <tr key={app.id}>
                    <td className="am-cell am-name am-primaryName"><Link className="am-app" to={"/apps/" + app.id}>{app.name}</Link></td>
                    <td className="am-cell am-repo">{app.repo}</td>
                    <td className="am-cell am-status">{app.status}</td>
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

export default Applications;