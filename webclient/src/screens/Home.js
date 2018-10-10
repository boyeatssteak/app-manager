import React from 'react';

import PageHeader from '../components/PageHeader';

class Home extends React.Component {

  render() {
    return (
      <div className="container">
        <PageHeader title="App Manager" />
        <p>Stuff and nonsense.</p>
      </div>
    );
  }
}

export default Home;