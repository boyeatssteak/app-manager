import React from 'react';

import Icon from './Icon';

class Heading extends React.Component {

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
        <div>
          <Icon size="small" itemType={this.props.itemType} />
          <h3 className={"am-section am-" + this.props.itemType}>{this.props.title}</h3>
        </div>
      )
    }

    return(
      <h1>...no heading type?</h1>
    )

  }
}

export default Heading;