import React from 'react';
import { Link } from 'react-router-dom';

const Header = () => (
  <header className="container-fluid">
    <nav className="navbar fixed-top navbar-light bg-light">
      <div className="navbar-brand navTitle"><Link to="/">App Manager</Link></div>
      <ul className="nav justify-content-end nav-pills">
        <li className="nav-item"><Link className="nav-link" to="/servers">Servers</Link></li>
        <li className="nav-item"><Link className="nav-link" to="/apps">Applications</Link></li>
        <li className="nav-item"><Link className="nav-link" to="/platforms">Platforms</Link></li>
        <li className="nav-item"><Link className="nav-link" to="/search">Search</Link></li>
      </ul>
    </nav>
  </header>
);

export default Header;