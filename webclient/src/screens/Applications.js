import React from 'react';

import PageHeader from '../components/PageHeader';

class Applications extends React.Component {
  render() {
    return (
      <div className="container">
        <PageHeader title="Applications" />
        <ApplicationsContent fetchData={this.props.fetchData} />
      </div>
    )
  }
}

class ApplicationsContent extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      hasLoaded: false,
      hasErrored: false
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
        <div>
          <ul>
            {this.state.response.map((app) => (
              <li key={app.id}>
                {app.name} - {app.status}
              </li>
            ))}
          </ul>
        </div>
      )
    }

    return <p>Loading...</p>;

  }
}

export default Applications;