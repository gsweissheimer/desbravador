import React, { useEffect, useState } from 'react';
import { Layout, } from 'antd';
import { CreateUserProps, User } from '../../types';
import axios from 'axios';

const CreateUser: React.FC<CreateUserProps> = ({ handleCreateUser }) => {
  const [newRandomUser, setNewRandomUser] = useState<User | null>(null);
  const [getRandomUser, setGetRandomUser] = useState<boolean>(true);

  useEffect(() => {
    if (getRandomUser) {
      setGetRandomUser(false)
      GetUser();
    }
  }, []);
  const GetUser = () => {
    axios.get('http://localhost:5130/api/RandomUser' )
      .then(response => {
        setNewRandomUser(response.data)
        handleCreateUser(response.data)
      })
      .catch(error => {
        console.error('Error:', error);
      });
  };

    return (
      <>
        <Layout style={{ padding: '0 24px 24px'  }}>
            
              {<>
                <h4>Nome: {newRandomUser?.firstName ?? ''} {newRandomUser?.lastName ?? ''}</h4>
                <h5>Email: {newRandomUser?.email ?? ''}</h5>
              </>}
            
        </Layout>
        <p>Você gostaria de criar esse usuário?</p>
      </>
    );
};

export default CreateUser;