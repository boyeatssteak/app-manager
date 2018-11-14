import React from 'react';

import LargeIcon from '../components/LargeIcon';

class Home extends React.Component {

  render() {
    return (
      <div className="container">
        <LargeIcon itemType="home" />
        <section className="am-container am-primary">
          <h1>App Manager</h1>
          <p>This is a place to help you figure out what applications are on what servers, or what URL is used to manage ___ app.</p>
        </section>
      </div>
    );
  }
}

export default Home;