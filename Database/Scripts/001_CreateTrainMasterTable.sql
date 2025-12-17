-- Migration: 001_CreateTrainMasterTable
-- Description: Creates the train_master table
-- Date: 2025-12-14

CREATE TABLE IF NOT EXISTS train_master (
    train_id SERIAL PRIMARY KEY,
    train_no VARCHAR(10) NOT NULL UNIQUE,
    train_name VARCHAR(100),
    last_updated TIMESTAMP DEFAULT NOW()
);

-- Create unique index on train_no (already included in UNIQUE constraint, but explicit for clarity)
CREATE INDEX IF NOT EXISTS idx_train_master_train_no ON train_master(train_no);




