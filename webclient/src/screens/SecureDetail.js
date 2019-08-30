import React from 'react';
import { Link } from 'react-router-dom';

import Loading from '../components/Loading';
import Heading from '../components/Heading';

class SecureAreaDetail extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      hasLoaded: false,
      hasErrored: false,
      itemType: "secure",
      itemDisplayName: "Secure Area Detail"
    }
  }

  async componentDidMount() {
    this.props.fetchData(`/api/secureareas/${this.props.match.params.secureId}`, this )
  }

  render() {
    if (this.state.hasErrored) {
      return <p>Error'd!</p>;
    }

    if (this.state.hasLoaded) {
      return (
        <div className={"container am-" + this.state.itemType}>
          <Heading headingType="detail" itemType={this.state.itemType} itemDisplayName={this.state.itemDisplayName} title={this.state.response.description} />
          <section className="am-container">
            <p><span className="am-detailTitle">URL:</span><a href={this.state.response.url} target="_blank">{this.state.response.url}</a></p>
            <p><span className="am-detailTitle">AppID:</span>{this.state.response.appId}</p>
            <p><span className="am-detailTitle">OwnerID:</span>{this.state.response.ownerId}</p>
          </section>
        </div>
      )
    }

    return <Loading loadingItem={this.state.itemDisplayName} />;

  }
}

export default SecureAreaDetail;