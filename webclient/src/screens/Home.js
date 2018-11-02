import React from 'react';

import PageHeader from '../components/PageHeader';

class Home extends React.Component {

  render() {
    return (
      <div className="container">
        <PageHeader title="App Manager" />
        <p>This is a place to help you figure out what applications are on what servers, or what URL is used to manage ___ app.</p>
      </div>
    );
  }
}

export default Home;