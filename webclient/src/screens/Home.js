import React from 'react';

import PageHeader from '../components/PageHeader';

class Home extends React.Component {

  render() {
    return (
      <div className="container">
        <PageHeader title={this.props.projectName} />
        <p>{this.props.projectDesc}</p>
      </div>
    );
  }
}


export default Home;