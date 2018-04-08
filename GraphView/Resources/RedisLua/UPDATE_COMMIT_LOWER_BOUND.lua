﻿-- eval lua_script 1 txId commit_time -1 -2
local commit_time = ARGV[1]
local negative_one = ARGV[2]
local negative_two = ARGV[3]

local data = redis.call('HMGET', KEYS[1], 'commit_time', 'commit_lower_bound')
if not data then
    return negative_two
end

local tx_commit_time = data[1]
local commit_lower_bound = data[2]

if tx_commit_time == negative_one and 
    string.byte(commit_lower_bound) < string.byte(commit_time) then
    local ret = redis.call('HSET', KEYS[1], 'commit_lower_bound', commit_time)
    if ret ~= 0 then
        return negative_two
    end
end

return tx_commit_time