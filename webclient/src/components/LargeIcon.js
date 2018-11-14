import React from 'react';

class LargeIcon extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      icon: "fas fa-square"
    }
  }

  componentDidMount() {
    switch(this.props.itemType) {
      case 'app':
        this.setState({ icon: "fas fa-desktop" });
        break;
      case 'instance':
        this.setState({ icon: "fas fa-clone" });
        break;
      case 'platform':
        this.setState({ icon: "fas fa-layer-group" });
        break;
      case 'secure':
        this.setState({ icon: "fas fa-lock" });
        break;
      case 'server':
        this.setState({ icon: "fas fa-server" });
        break;
      case 'user':
        this.setState({ icon: "fas fa-user" });
        break;
      case 'vendor':
        this.setState({ icon: "fas fa-building" });
        break;
      default:
        this.setState({ icon: "fas fa-square" });
    }
  }

  render() {

    return (
      <div className={"am-largeIcon am-" + this.props.itemType}>
        <i className={this.state.icon}></i>
      </div>
    )
  }
}

export default LargeIcon;