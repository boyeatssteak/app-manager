import React from 'react';

class Icon extends React.Component {

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

    if(this.props.size === 'large') {
      return (
        <div className={"am-largeIcon am-" + this.props.itemType}>
          <i className={this.state.icon}></i>
        </div>
      )
    }

    if(this.props.size === 'small') {
      return (
        <div className={"am-smallIcon am-" + this.props.itemType}>
          <i className={this.state.icon}></i>
        </div>
      )
    }

    return (
      <i className={this.state.icon}></i>
    )

  }
}

export default Icon;