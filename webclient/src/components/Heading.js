import React from 'react';
import { Link } from 'react-router-dom';

import Icon from './Icon';

class Heading extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      addName: ""
    }
  }

  componentDidMount() {
    switch(this.props.itemType) {
      case 'app':
        this.setState({ addName: "Application" });
        break;
      case 'instance':
        this.setState({ addName: "Instance" });
        break;
      case 'platform':
        this.setState({ addName: "Platform" });
        break;
      case 'secure':
        this.setState({ addName: "Secure Area" });
        break;
      case 'server':
        this.setState({ addName: "Server" });
        break;
      case 'user':
        this.setState({ addName: "Person" });
        break;
      case 'vendor':
        this.setState({ addName: "Vendor" });
        break;
      default:
        this.setState({ addName: "" });
    }
  }

  render() {

    if (this.props.headingType === 'main') {
      return(
        <div>
          <Icon size="large" itemType={this.props.itemType} />
          <section className="am-container am-primary">
            <h1 className={"am-" + this.props.itemType}>{this.props.itemDisplayName}</h1>
          </section>
        </div>
      )
    }

    if (this.props.headingType === 'detail' && !this.props.domain) {
      return(
        <div>
          <Icon size="large" itemType={this.props.itemType} />
          <section className="am-container am-primary">
            <h3>{this.props.itemDisplayName}</h3>
            <h2 className={"am-" + this.props.itemType}>
              {this.props.title}
            </h2>
          </section>
        </div>
      )
    }

    if (this.props.headingType === 'detail' && this.props.domain) {
      return(
        <div>
          <Icon size="large" itemType={this.props.itemType} />
          <section className="am-container am-primary">
            <h3>{this.props.itemDisplayName}</h3>
            <h2 className={"am-" + this.props.itemType}>
              {this.props.title}
              <span className="am-domain">.{this.props.domain}</span>
            </h2>
          </section>
        </div>
      )
    }

    if (this.props.headingType === 'related') {
      return(
        <div className="am-heading">
          <Icon size="small" itemType={this.props.itemType} />
          <h3 className={"am-section am-" + this.props.itemType}>{this.props.title}
            {this.props.displayAddNew ?
            <Link to="#" className="am-addItem"><span><i class="fas fa-plus"></i> Add New {this.state.addName}</span></Link> : ""
            }
          </h3>
        </div>
      )
    }

    return(
      <h1>...no heading type?</h1>
    )

  }
}

export default Heading;