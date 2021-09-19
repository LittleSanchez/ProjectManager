import {
  Navbar,
  Nav,
  NavDropdown,
  Form,
  FormControl,
  Button,
} from "react-bootstrap";
import Link from 'next/link';
import React from "react";
import { motion } from "framer-motion";

function Layout({children}) {
  return (
    <>
      <Navbar bg="light" expand="lg">
        <Link href="/">
          <a className="navbar-brand">DreamFlat</a>
        </Link>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mr-auto">
            <Link href="/listposts">
              <a className="nav-link">Posts</a>
            </Link>
            <Link href="/create">
              <a className="nav-link">Create post</a>
            </Link>
            <Link href="/login">
              <a className="nav-link">Log in</a>
            </Link>
            <Link href="/registration">
              <a className="nav-link">Sign up</a>
            </Link>
          </Nav>
          {/* <Form inline>
            <FormControl type="text" placeholder="Search" className="mr-sm-2" />
            <Button variant="outline-secondary">Search</Button>
          </Form> */}
        </Navbar.Collapse>
      </Navbar>
      <motion.div>
        {children}
      </motion.div>
    </>
  );
}

export default Layout;
