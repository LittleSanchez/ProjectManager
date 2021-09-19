import Layout from "../_layout";
import Card from "react-bootstrap/Card";
import {Col, Row} from "react-bootstrap";
import {useRouter, withRouter} from 'next/router';
import postRepository from "../../helpers/PostRepository";
import { motion } from "framer-motion";
import React, { useEffect, useState } from 'react';
import { Button, CardColumns, CardDeck, Modal } from "react-bootstrap";


function PageDetails({post, handleClose}) {
    
    return (
        <>
            <Modal show onHide={handleClose} dialogClassName="modal-lg">
                <Modal.Header className="border-0">
                    
                </Modal.Header>
                <Modal.Body>
                    <motion.div
                        className="card border-0"
                        layoutId={`card-body${post?.id}`}
                    >
                        <Row className="no-gutters">
                            <Col md={5} lg={5}>
                                {/* <Card.Img
                            variant="top"
                            src="https://q-xx.bstatic.com/xdata/images/hotel/840x460/76109618.jpg?k=ba537b048279407e0241cbd138c6dced32572a4f864bdaf5dbb60c314c3003b0&o="
                        /> */}
                                <motion.img
                                    className="card-img-top"
                                    src={post?.imageUrl}
                                    layoutId={`img${post?.id}`}
                                />
                            </Col>
                            <Col>
                                <Card.Body>
                                    <motion.h4
                                        className="card-title"
                                        layoutId={`title${post?.id}`}
                                    >
                                        {post?.name}
                                    </motion.h4>
                                    {/* <Card.Subtitle tag="h6" className="mb-2 text-muted">
                                {post?.price}
                            </Card.Subtitle> */}

                                    <motion.h6
                                        className="card-subtitle"
                                        layoutId={`price${post?.id}`}
                                    >
                                        {post?.price}
                                    </motion.h6>

                                    <Card.Text>
                                        <div className="d-flex text-secondary justify-content-between">
                                            <motion.div
                                                layoutId={`bedrooms${post?.id}`}
                                            >
                                                <i className="fas fa-bed"></i>{" "}
                                                {post?.bedrooms}
                                                bedrooms
                                            </motion.div>
                                            <motion.div
                                                layoutId={`bathrooms${post?.id}`}
                                            >
                                                <i className="fas fa-bath"></i>{" "}
                                                {post?.bathrooms}
                                                bathrooms
                                            </motion.div>
                                        </div>
                                    </Card.Text>
                                    <p
                                        className="card-text"
                                        layoutId={`description${post?.id}`}
                                    >
                                        {post?.description}
                                    </p>
                                    <Button variant="primary" type="submit" className="mr-2">
                                        Add to cart
                                    </Button>
                                    <Button
                                        variant="secondary"
                                        onClick={handleClose}
                                    >
                                        Close
                                    </Button>
                                </Card.Body>
                            </Col>
                        </Row>
                    </motion.div>
                </Modal.Body>
            </Modal>
        </>
    );
}




PageDetails.Layout = Layout;

export default PageDetails;






//Query and page PageDatails---------------------------------


// function PageDetails(props) {
//     const router = useRouter();
//     const [post, setPost] = useState(null);

//     useEffect(async () => {
//         const {query} = router;
//         console.log(router);
//         if (query.id && !post) {
//             let post = await postRepository.getById(query.id);
//             console.log(post);
//             setPost(post);
//         }
//     }); 
//     if (!router.query.id){
//         return null;
//     }


//     return (
//         <>
//             <motion.div className="card m-4" layoutId={`card-body${router?.query?.id}`}>
//                 <Row className="no-gutters">
//                     <Col md={5} lg={5}>
//                         {/* <Card.Img
//                             variant="top"
//                             src="https://q-xx.bstatic.com/xdata/images/hotel/840x460/76109618.jpg?k=ba537b048279407e0241cbd138c6dced32572a4f864bdaf5dbb60c314c3003b0&o="
//                         /> */}
//                         <motion.img className="card-img-top" src={post?.imageUrl} layoutId={`img${router?.query?.id}`}/>
//                     </Col>
//                     <Col>
//                         <Card.Body>
//                         <motion.h4 className="card-title" layoutId={`title${router?.query?.id}`}>{post?.name}</motion.h4>
//                             {/* <Card.Subtitle tag="h6" className="mb-2 text-muted">
//                                 {post?.price}
//                             </Card.Subtitle> */}

//                             <motion.h6 className="card-subtitle" layoutId={`price${router?.query?.id}`}>{post?.price}</motion.h6>

//                             <Card.Text>
//                                 <div className="d-flex text-secondary justify-content-between">
//                                     <motion.div layoutId={`bedrooms${router?.query?.id}`}>
//                                         <i className="fas fa-bed"></i> {post?.bedrooms}
//                                         bedrooms
//                                     </motion.div>
//                                     <motion.div layoutId={`bathrooms${router?.query?.id}`}>
//                                         <i className="fas fa-bath"></i> {post?.bathrooms}
//                                         bathrooms
//                                     </motion.div>
//                                 </div>
//                             </Card.Text>
//                             <p className="card-text" layoutId={`description${post?.id}`}>{post?.description}</p>
//                             <Button variant="secondary" type="submit">
//                                 Add to cart
//                             </Button>
//                         </Card.Body>
//                     </Col>
//                 </Row>
//             </motion.div>
//         </>
//     );
// }




// PageDetails.Layout = Layout;

// export default PageDetails;