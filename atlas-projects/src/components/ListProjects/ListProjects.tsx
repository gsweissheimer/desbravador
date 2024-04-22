import React, { useEffect, useState } from 'react';
import { Table, Layout, Button } from 'antd';
import axios from "axios";

import { Project, ListProjectsProps, AtlasState } from '../../types';
import { useSelector } from 'react-redux';

const { Content } = Layout;

const columns = [
  {
    title: 'Name',
    dataIndex: 'name',
    key: 'name',
    render: (text:string) => <Button type='link'  onClick={() => {}}>{text}</Button>,
  },
  {
    title: 'Description',
    dataIndex: 'description',
    key: 'description',
  }
];

const ListProjects: React.FC<ListProjectsProps> = ({ OpenProjectDetail }) => {
    
    const users = useSelector((state: AtlasState) => state.atlas.users);
    const projects = useSelector((state: AtlasState) => state.atlas.projects);

    const handleOpenProjectDetail = (type : string, key: React.Key) => {
        OpenProjectDetail(type, key)
      };

    return (
        <Layout style={{ padding: '0 24px 24px'  }}>
            <Content
                className="site-layout-background"
                style={{
                padding: 24,
                margin: 0,
                minHeight: 280,
                }}
            >
                <h2>Listar Projeto</h2>
                {projects && <Table<Project>
                    dataSource={projects}
                    columns={columns}
                    pagination={false}
                    />}
                
                
            </Content>
        </Layout>
    );
};

export default ListProjects;